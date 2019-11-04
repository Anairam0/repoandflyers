using System;
using System.Linq;
using System.Collections.Generic;

// To execute C#, please define "static void Main" on a class
// named Solution.

class Solution
{
    static void Main(string[] args)
    {

    }
}

public class Flyer
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class Click
{
    public string Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string FlyerId { get; set; }
}

public class FlyerRepository
{
    public IList<Flyer> listFlyer = new List<Flyer>();

    public void AddElement(Flyer flyer)
    {
        listFlyer.Add(flyer);
    }

    public Flyer GetElement(string id)
    {
        return listFlyer.FirstOrDefault(f => f.Id == id);
    }
}

public class ClickRepository
{
    public int maxClick { get; set; }
    public int secondsDelay { get; set; }

    public IList<Click> listClicks = new List<Click>();

    public ClickRepository()
    {
        this.maxClick = 3;
        this.secondsDelay = 5;
    }

    public void AddElement(string flyerId)
    {
        if (this.PreventClick(flyerId))
        {
            var newClick = new Click()
            {
                Id = Guid.NewGuid().ToString(),
                FlyerId = flyerId,
                Timestamp = DateTime.Now
            };

            listClicks.Add(newClick);
        }
    }

    public Click GetElement(string id)
    {
        return this.listClicks.FirstOrDefault(f => f.Id == id);
    }

    public Click GetElementsByFlyerId(string flyerId)
    {
        return this.listClicks.FirstOrDefault(f => f.FlyerId == flyerId);
    }

    //For this last phase we want to support a mechanism to avoid adding too many clicks in a short time frame.
    //New requirement:
    //When adding a new 'click', only add it if there have been up to ‘X’ clicks added in the last ‘Y’ seconds.Otherwise print 'Unsuccessful' and do not add it.
    private bool PreventClick(string flyerId)
    {
        var numberClicks = this.listClicks.Where(t => t.FlyerId == flyerId
            && t.Timestamp >= DateTime.Now.AddSeconds(-this.secondsDelay)).Count();

        if (numberClicks < this.maxClick)
        {
            return true;
        }
        else
        {
            Console.WriteLine("Unsuccessful");
            return false;
        }
    }

    /*Then, implement a way to get the most clicked flyer between two given timestamps.*/

    public string GetMostClickedFlyer(DateTime stamp1, DateTime stamp2)
    {
        var filterClickList = this.listClicks.Where(x => x.Timestamp > stamp1 && x.Timestamp < stamp2);

        return filterClickList.GroupBy(i => i.FlyerId)
            .Select(group => new { FlyerId = group.Key, Count = group.Count() })
            .OrderByDescending(y => y.Count).FirstOrDefault().FlyerId;
    }
}

/* 
Your previous Plain Text content is preserved below:

This is just a simple shared plaintext pad, with no execution capabilities.

When you know what language you'd like to use for your interview,
simply choose it from the dropdown in the top bar.

You can also change the default language your pads are created with
in your account settings: https://coderpad.io/settings

Enjoy your interview!

Duration: 90 minutes
* You can use any of the languages that you are most comfortable in.  
* Each phase is linked to the previous one, please complete phase by phase.
* Consider this as if you are writing PRODUCTION level code.
* You are free to use the internet - Google, Stack Overflow, etc. is your friend.
* If you get stuck or have a question, make an assumption, document it and continue.
* Make sure your code is runnable.


Background Information: 
At Flipp, we process a lot of flyers every day.
For this question, we'd like you to implement some operations related to flyers.

(Phase 1)
1. Create a structure (classes, methods or functions) consisting of a collection of flyers where each flyer has an 'id'.
2. Then, implement a way to add a flyer to the collection and also retrieve the flyer given an 'id'.

(Phase 2)
Now, consider that a flyer can also have any number of 'clicks' associated with it where each click has a 'timestamp'
indicating when that flyer was clicked.
1. Implement a way to add and retrieve clicks for a given flyer.
2. Then, implement a way to get the most clicked flyer between two given timestamps.

Example:
Flyer 1 has click at timestamp 1 and 10
Flyer 2 has clicks at timestamp 1, 2, 3, and 12
Calling this method with timestamps 1 and 11 should return flyer 2 (3 clicks > 2 clicks).

(Phase 3)
For this last phase we want to support a mechanism to avoid adding too many clicks in a short time frame.
New requirement:
When adding a new 'click', only add it if there have been up to ‘X’ clicks added in the last ‘Y’ seconds. Otherwise print 'Unsuccessful' and do not add it.

Example: 
Let ‘X’ = 3 clicks and ‘Y’ = 5 seconds.
Here, a Flyer F1 has 3 clicks already added in the last 5 seconds.
Click_id  |  Timestamp  |  Operation |
      101             1               (added)
      102             2               (added)
      103             3               (added)
      104             5         (Unsuccessful)
When a new click comes in at the 5th second, we do not add it.

 */
