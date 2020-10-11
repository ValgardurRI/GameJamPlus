using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandWheel : MonoBehaviour
{
    class Command
    {
        public float CommandCooldownTime;
        public float CommandCooldownTimer;
        public CommandType type;
        public Command(float cct, CommandType t)
        {
            CommandCooldownTime = cct;
            CommandCooldownTimer = 0;
            type = t;
        }
    }

    enum CommandType
    {
        PlantForestation,
        PlantMelee,
        PlantTurret,
        Attack
    }
    public float CommandChangeTime = 1f;
    
    private PlayerCharacter player;
    private float commandChangeTimer = 0f;
    Command nextCommand;
    Command selectedCommand;
    List<Command> commands;

    void Start()
    {
        player = Planet.Instance.GetComponentInChildren<PlayerCharacter>();
        commands= new List<Command>();
        commands.Add(new Command(5, CommandType.PlantForestation));
        commands.Add(new Command(5, CommandType.PlantMelee));
        commands.Add(new Command(5, CommandType.PlantTurret));
        commands.Add(new Command(5, CommandType.Attack));
        selectedCommand = commands[0];
    }
    
    void Update()
    {
        HandleInput();

        HandleRotation();

        HandleCooldown();
    }

    void HandleInput()
    {
        var swapCommandInput = (int)Input.GetAxisRaw("SwapCommand");
        if(swapCommandInput != 0)
        {
            var commandValue = (((int)selectedCommand.type + swapCommandInput)%4 + 4) % 4;
            setCommand(commandValue);
        }

        var jumpCommandInput = Input.GetAxisRaw("Jump");
        if(jumpCommandInput != 0 && selectedCommand.CommandCooldownTimer <= 0 && commandChangeTimer <= 0)
        {
            switch(selectedCommand.type)
            {
                case(CommandType.PlantForestation):
                    player.PlantForestation();
                    break;
                case(CommandType.PlantMelee):
                    player.PlantMelee();
                    break;
                case(CommandType.PlantTurret):
                    player.PlantTurret();
                    break;
                case(CommandType.Attack):
                    player.Attack();
                    break;
            }
            selectedCommand.CommandCooldownTimer = selectedCommand.CommandCooldownTime;
        }
    }

    void HandleRotation()
    {
        if(commandChangeTimer > 0)
        {
            commandChangeTimer -= Time.deltaTime;
            var currEulerAngles = transform.eulerAngles;
            var progress = 1 - commandChangeTimer/CommandChangeTime;
            currEulerAngles.z = Mathf.LerpAngle(commandToAngle(selectedCommand.type), commandToAngle(nextCommand.type), progress);
            transform.eulerAngles = currEulerAngles;
            
            if(commandChangeTimer <= 0)
            {
                commandChangeTimer = 0;
                selectedCommand = nextCommand;
            } 
        }
    }

    void HandleCooldown()
    {
        for(int i = 0; i < commands.Count; i++)
        {
            commands[i].CommandCooldownTimer -= Time.deltaTime;
            commands[i].CommandCooldownTimer = Mathf.Clamp(commands[i].CommandCooldownTimer, 0, commands[i].CommandCooldownTime);
            var image = transform.GetChild(i).GetComponent<Image>();
            var imageColor = image.color; 
            imageColor.a = 1 - commands[i].CommandCooldownTimer/commands[i].CommandCooldownTime;
            image.color= imageColor;
        }
    }


    void setCommand(int index)
    {
        if(commandChangeTimer == 0)
        {
            nextCommand = commands[index];
            commandChangeTimer = CommandChangeTime;
        }
    }

    float commandToAngle(CommandType command)
    {
        return ((int)command)*90f;
    }
}
