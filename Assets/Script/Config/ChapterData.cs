using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ChapterData 
{
    public int currentWave;
    public ChapterConfig chapterCfg;
    public event Action<ChapterWaveConfig> onNextWave;
    public event Action<int> onChapterFinish;
    public List<Enemy> currentEenemies;
    public ChapterData(ChapterConfig chapterCfg)
    {
        this.chapterCfg = chapterCfg;
        currentEenemies = new List<Enemy>();
        onNextWave += GenerateChapterWave;
    }

    public void WaveStart()
    {
        currentWave = 1;
        NextWave();
    }

    public void NextWave()
    {
        int index = (currentWave % chapterCfg.waves.Count);
        onNextWave?.Invoke(chapterCfg.waves[index]);
        currentWave++;
        if (currentWave<=chapterCfg.totalWaves)
        {
            int nextWave = GameManager._instance.timerManager.AddTimer(NextWave, chapterCfg.waves[index].waveInterval, 0);
        }
    }

    public void GenerateChapterWave(ChapterWaveConfig wave)
    {
        for (int i = 0; i < wave.enemyAmount; i++)
        {
            Enemy newEnemy = GameManager._instance.unitMgr.CreateEnemy(wave.enemyID);
            currentEenemies.Add(newEnemy);
            newEnemy.OnUnitDead += RemoveEnemy;
        }
    }

    public void RemoveEnemy(Unit enemy)
    {
        enemy.OnUnitDead -= RemoveEnemy;
        currentEenemies.Remove(enemy as Enemy);
        if(currentWave>= chapterCfg.totalWaves && currentEenemies.Count<=0)
        {
            onChapterFinish?.Invoke(chapterCfg.chapterID);
        }
    }
}
