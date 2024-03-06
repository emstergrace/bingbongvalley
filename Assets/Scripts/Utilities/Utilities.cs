using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static bool Contains(this Rect rect1, Rect rect2) {

        if ((rect1.position.x <= rect2.position.x) &&
            (rect1.position.x + rect1.size.x) >= (rect2.position.x + rect2.size.x) &&
            (rect1.position.y <= rect2.position.y) &&
            (rect1.position.y + rect1.size.y) >= (rect2.position.y + rect2.size.y)) {

            return true;
        }
        else {

            return false;
        }
    }
}
