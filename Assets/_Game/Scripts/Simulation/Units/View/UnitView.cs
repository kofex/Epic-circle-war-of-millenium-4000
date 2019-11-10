using Scripts.Core.View;
using Scripts.Simulation.Units.Model;
using UnityEngine;

namespace Scripts.Core.Model
{
	public class UnitView : ViewBase<UnitModel>
	{
		private SpriteRenderer _spriteRenderer;

		public SpriteRenderer SpriteRenderer => _spriteRenderer != null
			? _spriteRenderer
			: (_spriteRenderer = GetComponent<SpriteRenderer>());

	}
}