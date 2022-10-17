using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bottom : UI_Base
{
    enum Buttons
    {
        SkillEnhancementButton,
        StageAndBossButton,
        EquipmentFortificationButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<Button>(typeof(Buttons));

        return true;
    }
}
