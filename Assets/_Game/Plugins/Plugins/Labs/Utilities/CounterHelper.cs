using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHelper : MonoBehaviourSingleton<CounterHelper>
{
	struct TimedAction
	{
		public float executeTime;
		public float delayTime;
		public Action action;
		public int loopTimes;
	}

	List<TimedAction> queuedActions = new List<TimedAction>();
	public void QueueAction(float delayInSeconds, Action actionToExecute, int loopTime = 1)
	{
		TimedAction newTimedAction = new TimedAction()
		{
			delayTime = delayInSeconds,
			executeTime = Time.time + delayInSeconds,
			action = actionToExecute,
			loopTimes = loopTime
		};

		for (int x = 0; x <queuedActions.Count; x++)
		{
			
			if (newTimedAction.executeTime < queuedActions[x].executeTime)
			{
				queuedActions.Insert(x, newTimedAction);

				return;
			}
		}

		queuedActions.Add(newTimedAction);
    }
	public void CountTime(float time,float timeEnd, Action action = null, Action actionEnd = null)
    {
		time--;
		if (time >= timeEnd)
		{
			action?.Invoke();
			QueueAction(1f, (() =>
			{
				CountTime(time, timeEnd, action);
			}));
		}
		else
			actionEnd?.Invoke();
    }

	void Update()
	{
		while (queuedActions.Count > 0)
		{
			if (Time.time >= queuedActions[0].executeTime)
			{
				queuedActions[0].action();
				var item = queuedActions[0];
				item.loopTimes -= 1;
				queuedActions.RemoveAt(0);
				if (item.loopTimes != 0)
                {
					this.QueueAction(item.delayTime, item.action, item.loopTimes);
				}
        
			}
			else
			{
				break;
			}
		}
	}

}

