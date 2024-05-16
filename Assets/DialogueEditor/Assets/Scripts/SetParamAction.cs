using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueEditor
{
    public abstract class SetParamAction
    {
        public enum eParamActionType
        {
            Int,
            Bool,
            Quest
        }

        public abstract eParamActionType ParamActionType { get; }

        public string ParameterName;
    }

    public class SetIntParamAction : SetParamAction
    {
        public override eParamActionType ParamActionType { get { return eParamActionType.Int; } }

        public int Value;
    }

    public class SetBoolParamAction : SetParamAction
    {
        public override eParamActionType ParamActionType { get { return eParamActionType.Bool; } }

        public bool Value;
    }

    public class SetQuestParamAction : SetParamAction
    {
        public override eParamActionType ParamActionType { get { return eParamActionType.Quest; } }

        public QuestStatus Status;
    }
}
