using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson7.Game.Abstract
{
    interface IKeepable
    {
        bool IsPlayerKeeper { get; set; }
        bool IsEnemyKeeper { get; set; }
        bool IsItemKeeper { get; set; }
    }
}
