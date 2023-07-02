using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAct 
{
    List<GameObject> listEmojis { get; set; }

    void StartActing();
    void ChangeEmoji(int n);
}
