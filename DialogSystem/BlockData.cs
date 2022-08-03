using System.Collections.Generic;
namespace DialogSystem;

public enum BlockType
{
    Invalid,
    Text,
    Option
}
public class BlockData
{
    public readonly BlockType DataType = BlockType.Invalid;

    public readonly List<DialogSystem.DialogActionBase> Actions = new List<DialogActionBase>();

    public BlockData(BlockType type, List<DialogSystem.DialogActionBase> actions)
    {
        DataType = type;
        Actions = actions;
    }
}