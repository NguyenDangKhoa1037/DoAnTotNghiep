using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effection : MonoBehaviour
{
    /// check push on stask effection
    virtual public bool canPushed(EffectionManager manager) {
        return true;
    }

    virtual public void startEffect() { 
    
    }

}
