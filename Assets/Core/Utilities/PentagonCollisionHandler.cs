using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagonCollisionHandler : TriggerCollisionHandler
{
    protected override bool IsWinner(bool IsSimpleMode, string triggerTag, string otherTag) 
    {
    	if (IsSimpleMode || IsInvulnerable)
	        return base.IsWinner(IsSimpleMode, triggerTag, otherTag);
	    else 
	        return (triggerTag == "Cross" && otherTag == "Square") ||
                   (triggerTag == "Cross" && otherTag == "Star") ||
                   (triggerTag == "Square" && otherTag == "Circle") ||
                   (triggerTag == "Square" && otherTag == "Pyramid") ||
                   (triggerTag == "Circle" && otherTag == "Cross") ||
                   (triggerTag == "Circle" && otherTag == "Star") ||
                   (triggerTag == "Pyramid" && otherTag == "Circle") ||
                   (triggerTag == "Pyramid" && otherTag == "Cross") ||
                   (triggerTag == "Star" && otherTag == "Pyramid") ||
                   (triggerTag == "Star" && otherTag == "Square");
    }
    
    protected override bool IsLoser(bool IsSimpleMode, string triggerTag, string otherTag)
    {
    	if (IsSimpleMode || IsInvulnerable)
	        return base.IsLoser(IsSimpleMode, triggerTag, otherTag);
	    else
	        return (triggerTag == "Square" && otherTag == "Cross") ||
                   (triggerTag == "Star" && otherTag == "Cross") ||
                   (triggerTag == "Circle" && otherTag == "Square") ||
                   (triggerTag == "Pyramid" && otherTag == "Square") ||
                   (triggerTag == "Cross" && otherTag == "Circle") ||
                   (triggerTag == "Star" && otherTag == "Circle") ||
                   (triggerTag == "Circle" && otherTag == "Pyramid") ||
                   (triggerTag == "Cross" && otherTag == "Pyramid") ||
                   (triggerTag == "Pyramid" && otherTag == "Star") ||
                   (triggerTag == "Square" && otherTag == "Star");;
    }
}