using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCollisionHandler : TriggerCollisionHandler
{
    protected override bool IsWinner(bool IsSimpleMode, string triggerTag, string otherTag) 
    {
    	if (IsSimpleMode || IsInvulnerable)
	        return base.IsWinner(IsSimpleMode, triggerTag, otherTag);
	    else 
	        return (triggerTag == "Cross" && otherTag == "Square") ||
                   (triggerTag == "Square" && otherTag == "Circle") ||
                   (triggerTag == "Circle" && otherTag == "Cross");
    }
    
    protected override bool IsLoser(bool IsSimpleMode, string triggerTag, string otherTag)
    {
    	if (IsSimpleMode || IsInvulnerable)
	        return base.IsLoser(IsSimpleMode, triggerTag, otherTag);
	    else
	        return (triggerTag == "Cross" && otherTag == "Circle") ||
                   (triggerTag == "Square" && otherTag == "Cross") ||
                   (triggerTag == "Circle" && otherTag == "Square");
    }
}
