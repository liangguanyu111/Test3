using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

class Timer
{
    public TimerMgr.TimerHandler callback;  //回调
    public float repeatRate; // 定时器触发的时间间隔;
    public float time; // 第一次触发要隔多少时间;
    public int repeatTime; // 你要触发的次数,-1代表一直重复
    public float passedTime; // 这个Timer过去的时间;
    public bool isRemoved; // 是否已经删除了
    public int timerId; // 标识这个timer的唯一Id号;
}

//管理所有的定时器
public class TimerMgr 
{
    public delegate void TimerHandler();
    private Dictionary<int, Timer> timers = null;//存放Timer对象

    private List<Timer> removeTimers = null;
    private List<Timer> newAddTimers = null;

    private int autoIncId = 1;//每个Timer的唯一标示
    public TimerMgr()
    {
        timers = new Dictionary<int, Timer>();
        removeTimers = new List<Timer>();
        newAddTimers = new List<Timer>();
        autoIncId = 1;
    }


    public int AddTimer(TimerHandler callback, float time, float repeatRate, int repeat = 1)
    {
        Timer timer = new Timer();
        timer.callback = callback;
        timer.repeatTime = repeat;  
        timer.repeatRate = repeatRate;
        timer.time = time;
        timer.passedTime = timer.repeatRate - timer.time; // 延迟调用
        timer.isRemoved = false;
        timer.timerId = autoIncId;
        autoIncId++;
        newAddTimers.Add(timer); // 加到缓存队列里面
        return timer.timerId;
    }
    public void RemoveTimer(int timerId)
    {
        if (!timers.ContainsKey(timerId))
        {
            return;
        }
        timers[timerId].isRemoved = true; // 先标记，不直接删除
    }

    public void Update()
    {
        float dt = Time.deltaTime;
        //分别在update逻辑之前和之后增删Timers
        for (int i = 0; i < newAddTimers.Count; i++)
        {
            timers.Add(newAddTimers[i].timerId, newAddTimers[i]);
        }
        newAddTimers.Clear();

        foreach (Timer timer in timers.Values)
        {
            if (timer.isRemoved)
            {
                removeTimers.Add(timer);
                continue;
            }

            timer.passedTime += dt;
            if(timer.passedTime>=timer.repeatRate)
            {
                timer.callback();
                timer.repeatTime--;
                timer.passedTime = 0;
                timer.time = 0;
            }
            if (timer.repeatTime == 0)
            {
                // 触发次数结束，将该删除的加入队列
                timer.isRemoved = true;
                removeTimers.Add(timer);
            }
        }


        for (int i = 0; i < removeTimers.Count; i++)
        {
            timers.Remove(removeTimers[i].timerId);
        }
        removeTimers.Clear();
    }

}
