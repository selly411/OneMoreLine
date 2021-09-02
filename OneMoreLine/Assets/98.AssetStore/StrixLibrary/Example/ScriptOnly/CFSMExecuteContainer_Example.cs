#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-04-21 오후 4:03:52
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

/// <summary>
/// 
/// </summary>
public class CFSMExecuteContainer_Example : CObjectBase
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    public enum EState
    {
        Idle,
        Chase,
        Avoid,
        Patrol,
        Recovery,
    }

    public class FSMExecuteContainer_Test : CFSMExecuterContainer<EState, ExampleStateBase, ExecuterBase, TransitionStateBase>
    {
        protected override void OnSetStateList_IfCountZero_IsError(ref List<ExampleStateBase> _list_ForPrint)
        {
            _list_ForPrint.Add(new Idle());
            _list_ForPrint.Add(new Chase());
            _list_ForPrint.Add(new Avoid());
            _list_ForPrint.Add(new Patrol());
            _list_ForPrint.Add(new Recovery());
        }
    }

    public abstract class ExampleStateBase : State_ExecuterContainer<EState, ExampleStateBase, ExecuterBase, TransitionStateBase>
    {
        public override EState IDictionaryItem_GetKey()
        {
            return GetType().GetFriendlyName().ConvertEnum<EState>();
        }

        public override IEnumerator OnStart_State_ExecuteContainer(ExampleStateBase pPrevState, EStateStartType eStateStartType)
        {
            yield break;
        }
    }

    public class Idle : ExampleStateBase
    {
    }

    public class Chase : ExampleStateBase
    {
    }

    public class Avoid : ExampleStateBase
    {
    }

    public class Patrol : ExampleStateBase
    {
    }

    public class Recovery : ExampleStateBase
    {
    }

    public abstract class TransitionStateBase : CTransitionStateExecuter_Base<EState>
    {
        public override string IHasName_GetName()
        {
            return this.ToStringSub();
        }
    }

    [RegistSubString("On Detect Enemy")]
    public class TransitionState_DetectEnemy : TransitionStateBase
    {
        public override bool CheckIsTransition()
        {
            return false;
        }
    }

    [RegistSubString("HP Is Lower")]
    public class TransitionState_HP_Low : TransitionStateBase
    {
        public int iHP_Percentage;

        public override bool CheckIsTransition()
        {
            return false;
        }
    }

    [RegistSubString("On Game Event")]
    public class TransitionState_OnGameEvent : TransitionStateBase
    {
        public enum EGameEvent
        {
            Player_Dead,
            Clear_Stage,
            Start_Stage
        }

        public EGameEvent eGameEvent;

        public override bool CheckIsTransition()
        {
            return false;
        }
    }

    public abstract class ExecuterBase : IExecuter
    {
        virtual public int p_iExecuterOrder => 0;

        public IExecuterList p_pOwnerList { get; set; }

        public void IExecuter_Check_IsInvalid_OnEditor(MonoBehaviour pScriptOwner, ref bool bIsInvalid_Default_IsFalse, ref string strErrorMessage_Default_Is_Error)
        {
        }

        public void IExecuter_OnAwake(IExecuterList pContainer, MonoBehaviour pScriptOwner)
        {
        }

        public void IExecuter_OnDestroy(IExecuterList pContainer, MonoBehaviour pScriptOwner)
        {
        }

        public void IExecuter_OnDisable(IExecuterList pContainer, MonoBehaviour pScriptOwner)
        {
        }

        public void IExecuter_OnEnable(IExecuterList pContainer, MonoBehaviour pScriptOwner)
        {
        }

        public string IHasName_GetName()
        {
            return this.ToStringSub();
        }
    }

    [RegistSubString("Recovery HP")]
    public class Execute_Recovery_HP : ExecuterBase
    {
        public int iHP_Percentage;
    }

    [RegistSubString("Play Buff")]
    public class Execute_Play_Buff : ExecuterBase
    {
        public enum EBuffName
        {
            Increase_Total_HP,
            Increase_Armor,
            Increase_MoveSpeed,
        }

        public EBuffName eBuffName;
    }

    [RegistSubString("Play Effect")]
    public class Execute_Play_Effect : ExecuterBase
    {
        public enum EEffectName
        {
            Fire,
            Explosion,
            Recovery,
            Stary,
        }

        public EEffectName eEffectName;
    }

    /* public - Field declaration            */

    public FSMExecuteContainer_Test Container;

    /* protected & private - Field declaration         */


    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/


    // ========================================================================== //

    /* protected - Override & Unity API         */


    /* protected - [abstract & virtual]         */


    // ========================================================================== //

#region Private

#endregion Private
}