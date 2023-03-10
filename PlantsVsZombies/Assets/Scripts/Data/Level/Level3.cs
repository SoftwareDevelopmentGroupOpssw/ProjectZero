using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 关卡3: 多多能量（天上掉落阳光的速度翻倍）
/// </summary>
public class Level3 : GridLevel
{
    private int row = 5;
    private int col = 10;
    private Sprite sprite;
    private float secondsBetweenFallingEnergy = 2.5f;//从天上掉落阳光的间隔时间

    private const float commonPossiblity = 0.3f;//普通僵尸概率
    private const float roadConePossiblity = 0.4f;//路障僵尸概率
    private const float bucketPossiblity = 0.3f;//铁桶僵尸概率

    private const int commonWeight = 1000;//普通僵尸权值
    private const int roadWeight = 2500;//路障僵尸权值
    private const int bucketWeight = 5000;//铁桶僵尸权值

    private const int waveCount = 4; // 僵尸波数
    private static int nowWave = 0; //已经生成的波数

    public Level3(Sprite sprite)
    {
        this.sprite = sprite;
    }
    public override int Row => row;

    public override int Col => col;

    public override Sprite Sprite => sprite;

    public override int StartEnergy { get; } = 50;

    protected override Vector2Int GridsLeftTopCornorPos => new Vector2Int(128, 128);

    protected override int GridWidth => 137;

    protected override int GridHeight => 134;

    private List<string> monsterNames = new List<string>()
    {
        "CommonZombie","CommonZombie","CommonZombie","RoadConeZombie","RoadConeZombie","BucketHeadZombie","BucketHeadZombie","BucketHeadZombie"
    };

    public override IMonsterData[] MonsterTypes
    {
        get
        {
            int index = 0;
            IMonsterData[] array = new IMonsterData[monsterNames.Count];
            foreach (string monsterName in monsterNames)
            {
                array[index++] = MonsterPrefabSerializer.Instance.GetMonsterData(monsterName);
            }
            return array;
        }
    }

    IEnumerator GenerateEnergy()
    {

        int offsetX = Screen.width / 10;
        int offsetY = Screen.height / 5;
        System.Random r = new System.Random();
        do //游戏进行中时一直有阳光掉落
        {
            yield return new WaitForSeconds(secondsBetweenFallingEnergy);

            Vector2 endPoint = new Vector2(r.Next(offsetX, Screen.width - offsetX), r.Next(offsetY, Screen.height - offsetY));
            Vector2 startPoint = new Vector2(endPoint.x, Screen.height);
            GameObject obj = EnergyMonitor.CreateEnergy(new Vector2Int((int)startPoint.x, (int)startPoint.y), EnergyType.Big); //从屏幕之外落下
            GameController.Instance.StartCoroutine(obj.GetComponent<Energy>().FallingCoroutine(obj, startPoint, endPoint));
        } while (GameController.Instance.IsGameStarted);
    }

    /// <summary>
    /// 关卡3的生怪策略——一定时间内随机刷一波怪
    /// </summary>
    /// <returns></returns>
    public override IEnumerator GenerateEnumerator()
    {
        nowWave = 0;
        GameController.Instance.StartCoroutine(GenerateEnergy());
        //第一只怪物出现时间
        yield return new WaitForSeconds(30f);
        AudioManager.Instance.PlayEffectAudio("awooga");

        int monsterTotalCount = 70;//怪物总数
        float geneateSpaceTime = 12f;//生成间隔
        int realMinGenerateCount = 1;//最小生成数量
        int realMaxGenerateCount = 5;//最大生成数量
        int fixedCount = 10; //（修正值）当关卡进度达到100%时 生成数量会增加这么多 
        int moreSummoned = 10; //大规模僵尸出现时，多生成的数量

        int nowGenerated = 0;
        int generateAmount = 0;

        System.Random r = new System.Random();
        while (nowWave < waveCount)
        {
            int minGenerateCount = realMinGenerateCount + nowGenerated * fixedCount / monsterTotalCount; //根据关卡进度修正后的最小生成数量
            int maxGenerateCount = realMaxGenerateCount + nowGenerated * fixedCount / monsterTotalCount; //根据关卡进度修正后的最大生成数量
            int totalWeight = nowGenerated * 1000 + 1000; //随着生成数量的增加 权值数也会越来越高
            bool isBigWave = false;
            Debug.Log(nowGenerated + "generated. now wave" + nowWave);
            if (waveCount * nowGenerated / monsterTotalCount != nowWave) // 当生成的怪物数量与总数相比达到1/waveCount时，会生成一次大规模僵尸，权值数变高，生成数量增加
            {
                isBigWave = true;
                totalWeight += totalWeight / 2; //权值变为1.5倍
                do
                {
                    generateAmount = r.Next(minGenerateCount + moreSummoned, maxGenerateCount + moreSummoned + 1);
                } while (generateAmount * commonWeight > totalWeight);//如果全部生成权值最小的普通僵尸仍会大于最大权重，则此次生成的generateAmout不合法
                //大规模僵尸忽略生怪限制
                nowWave++;
            }
            else//不是大规模 正常生成
            {
                do
                {
                    generateAmount = r.Next(minGenerateCount, maxGenerateCount + 1);
                } while (generateAmount * commonWeight > totalWeight);//如果全部生成权值最小的普通僵尸仍会大于最大权重，则此次生成的generateAmout不合法
                generateAmount = Mathf.Min(generateAmount, monsterTotalCount - nowGenerated);
            }

            var task = Task.Run<List<IMonsterData>>(() => CalculateFunction(totalWeight, generateAmount));
            while (!task.IsCompleted)
                yield return 1;
            if (isBigWave)
            {
                while (GameController.Instance.MonstersController.MonsterCount > 0)//大规模怪等到所有僵尸都死亡时才发动
                    yield return 1;
                AudioManager.Instance.PlayEffectAudio("hugewave"); //提示大规模怪
                yield return new WaitForSeconds(5f);//等候一段时间
                if (nowWave == waveCount) //最后一波
                {
                    AudioManager.Instance.PlayEffectAudio("finalwave");
                }
                AudioManager.Instance.PlayEffectAudio("siren");
                nowGenerated -= task.Result.Count; // 大规模出现的不算在关卡出怪中
                yield return MonsterPrefabSerializer.Instance.GetMonsterData("FlagZombie");//生成摇旗僵尸
            }
            foreach (IMonsterData tried in task.Result)
            {
                while (GameController.Instance.MonstersController.MonsterCount > maxGenerateCount) //大于最大生成数量 则等待
                    yield return 1;
                yield return tried;
            }
            nowGenerated += task.Result.Count;
            yield return new WaitForSeconds(geneateSpaceTime * (1 + nowGenerated / monsterTotalCount / 2));
        }
    }
    /// <summary>
    /// 利用权值计算怪物生产列表
    /// </summary>
    /// <param name="totalWeight"></param>
    /// <returns></returns>
    private List<IMonsterData> CalculateFunction(int totalWeight, int generateAmount)
    {
        int i = 0;
        List<IMonsterData> triedGenerated = new List<IMonsterData>();
        int nowWeight = 0;
        System.Random r = new System.Random();

        Debug.Log("total" + totalWeight + "amount " + generateAmount);
        do
        {
            if (totalWeight - nowWeight < bucketWeight && totalWeight - nowWeight < roadWeight)
            {
                while (totalWeight - nowWeight >= commonWeight && i < generateAmount)
                {
                    triedGenerated.Add(MonsterPrefabSerializer.Instance.GetMonsterData("CommonZombie"));
                    nowWeight += commonWeight;
                    i++;
                }
                //直接return 不再继续生成
                return triedGenerated;
            }
            double random = r.NextDouble();
            if (random < commonPossiblity)
            {
                triedGenerated.Add(MonsterPrefabSerializer.Instance.GetMonsterData("CommonZombie"));
                nowWeight += commonWeight;
            }
            else if (random < commonPossiblity + roadConePossiblity && totalWeight - nowWeight > roadWeight)
            {
                triedGenerated.Add(MonsterPrefabSerializer.Instance.GetMonsterData("RoadConeZombie"));
                nowWeight += roadWeight;
            }
            else if (random < commonPossiblity + roadConePossiblity + bucketPossiblity && totalWeight - nowWeight > bucketWeight)
            {
                triedGenerated.Add(MonsterPrefabSerializer.Instance.GetMonsterData("BucketHeadZombie"));
                nowWeight += bucketWeight;
            }
            i++;

        } while (i < generateAmount);
        return triedGenerated;
    }

}
