using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandWheel : MonoBehaviour
{
    enum CommandType
    {
        PlantForestation,
        PlantMelee,
        PlantTurret,
        Attack
    }
    public float CommandChangeTime = 1f;
    
    private float commandChangeTimer = 0f;
    CommandType nextCommand;
    CommandType selectedCommand;
    
    void Update()
    {
        var swapCommandInput = (int)Input.GetAxisRaw("SwapCommand");
        if(swapCommandInput != 0)
        {
            var commandValue = (CommandType)(((int)selectedCommand + swapCommandInput) % 4);
            setCommand(commandValue);
        }

        if(commandChangeTimer > 0)
        {
            commandChangeTimer -= Time.deltaTime;
            var currEulerAngles = transform.eulerAngles;
            var progress = 1 - commandChangeTimer/CommandChangeTime;
            currEulerAngles.z = Mathf.LerpAngle(commandToAngle(selectedCommand), commandToAngle(nextCommand), progress);
            transform.eulerAngles = currEulerAngles;
            
            if(commandChangeTimer <= 0)
            {
                commandChangeTimer = 0;
                selectedCommand = nextCommand;
            } 
        }
    }

    void setCommand(CommandType command)
    {
        if(commandChangeTimer == 0)
        {
            nextCommand = command;
            commandChangeTimer = CommandChangeTime;
        }
    }

    float commandToAngle(CommandType command)
    {
        return ((int)command)*90f;
    }
}
