using Stride.BepuPhysics.Components;
using Stride.Core.Mathematics;
using Stride.Core.Serialization;
using Stride.Engine;
using Stride.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsComparison.Code;
// Because this relies on bepu there needs to be a simple static conatiner and collider 
// in the scene to create the bepu processor or else this wont run.
public class PrefabSpawner : SimulationUpdateComponent
{
	public Prefab Prefab { get; set; }
	public InstancingComponent Instancing { get; set; }

	private Random _random = new();
	private int _spawnCount = 0;

	// due to thread safety this needs to be done in the simulation update or else it will eventually crash.
	public override void SimulationUpdate(float simTimeStep)
	{
		if (Game.UpdateTime.FramePerSecond > 30)
		{
			var entity = Prefab.Instantiate()[0];
			entity.Transform.Position = Entity.Transform.WorldMatrix.TranslationVector;
			var instance = entity.Get<InstanceComponent>();
			if (instance != null)
			{
				instance.Master = Instancing;
			}
			entity.Transform.Position += new Vector3(_random.Next(-10, 10), _random.Next(-10, 10), _random.Next(-10, 10));
			SceneSystem.SceneInstance.RootScene.Entities.Add(entity);
			_spawnCount++;
		}
	}

	public override void Update()
	{
		DebugText.Print($"Cubes: {_spawnCount}", new Int2(800, 10));
	}
}
