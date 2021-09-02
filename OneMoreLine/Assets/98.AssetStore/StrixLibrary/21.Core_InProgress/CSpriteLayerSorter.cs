﻿#region Header
/* ============================================ 
 *			    Strix Unity Library
 *		https://github.com/KorStrix/UnityLibrary
 *	============================================ 	
 *	관련 링크 :
 *	
 *	설계자 : 
 *	작성자 : Strix
 *	
 *	기능 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSpriteLayerSorter : CObjectBase
{
	/* const & readonly declaration             */

	/* enum & struct declaration                */

	public struct SDataSpriteOrder
	{
		public int iSortingOrder_Origin;
		public SpriteRenderer pSpriteRenderer;

		public SDataSpriteOrder(int iSortingOrder_Origin, SpriteRenderer pSpriteRenderer )
		{
			this.iSortingOrder_Origin = iSortingOrder_Origin;
			this.pSpriteRenderer = pSpriteRenderer;
		}
	}

	/* public - Field declaration            */

	/* protected - Field declaration         */

	/* private - Field declaration           */

	private List<SDataSpriteOrder> _listSpriteRenderer = new List<SDataSpriteOrder>();

	// ========================================================================== //

	/* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

	public void DoSetSpriteOrder_Origin()
	{
		for (int i = 0; i < _listSpriteRenderer.Count; i++)
		{
			SDataSpriteOrder pDataSpriteOrder = _listSpriteRenderer[i];
			pDataSpriteOrder.pSpriteRenderer.sortingOrder = pDataSpriteOrder.iSortingOrder_Origin;
		}
	}

	public void DoSetSpriteOrder(int iOrderOffset)
	{
		if (_listSpriteRenderer.Count == 0)
			Debug.LogWarning( name + "DoSetSpriteOrder Error _listSpriteRenderer.Count == 0", this );

		for (int i = 0; i < _listSpriteRenderer.Count; i++)
		{
			SDataSpriteOrder pDataSpriteOrder = _listSpriteRenderer[i];
			pDataSpriteOrder.pSpriteRenderer.sortingOrder = pDataSpriteOrder.iSortingOrder_Origin + iOrderOffset;
		}
	}

	/* public - [Event] Function             
       프랜드 객체가 호출(For Friend class call)*/

	// ========================================================================== //

	#region Protected

	/* protected - [abstract & virtual]         */

	/* protected - [Event] Function           
       자식 객체가 호출(For Child class call)		*/

	/* protected - Override & Unity API         */

	protected override void OnAwake()
	{
		base.OnAwake();

		SpriteRenderer[] arrSpriteRenderer = GetComponentsInChildren< SpriteRenderer>( true );
		for(int i = 0; i < arrSpriteRenderer.Length; i++)
			_listSpriteRenderer.Add( new SDataSpriteOrder( arrSpriteRenderer[i].sortingOrder, arrSpriteRenderer[i] ));
	}

	#endregion Protected

	// ========================================================================== //

	#region Private

	/* private - [Proc] Function             
       로직을 처리(Process Local logic)           */

	/* private - Other[Find, Calculate] Func 
       찾기, 계산등 단순 로직(Simpe logic)         */

	#endregion Private
}
