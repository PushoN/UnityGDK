// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Improbable.Worker.Core;
using Improbable.Gdk.Core;
using Improbable.Gdk.Core.CodegenAdapters;
using Improbable.Gdk.Core.Commands;

namespace Generated.Improbable.Gdk.Tests
{
    public partial class NestedComponent
    {
        public class DispatcherHandler : ComponentDispatcherHandler
        {
            public override uint ComponentId => 20152;

            private readonly EntityManager entityManager;

            private const string LoggerName = "NestedComponent.DispatcherHandler";


            public DispatcherHandler(Worker worker, World world) : base(worker, world)
            {
                entityManager = world.GetOrCreateManager<EntityManager>();
                var bookkeepingSystem = world.GetOrCreateManager<CommandRequestTrackerSystem>();
            }

            public override void Dispose()
            {
            }

            public override void OnAddComponent(AddComponentOp op)
            {
                if (!IsValidEntityId(op.EntityId, "AddComponentOp", out var entity))
                {
                    return;
                }

                var data = global::Generated.Improbable.Gdk.Tests.SpatialOSNestedComponent.Serialization.Deserialize(op.Data.SchemaData.Value.GetFields(), World);
                data.DirtyBit = false;
                entityManager.AddComponentData(entity, data);
                entityManager.AddComponentData(entity, new NotAuthoritative<SpatialOSNestedComponent>());

                if (entityManager.HasComponent<ComponentRemoved<SpatialOSNestedComponent>>(entity))
                {
                    entityManager.RemoveComponent<ComponentRemoved<SpatialOSNestedComponent>>(entity);
                }
                else if (!entityManager.HasComponent<ComponentAdded<SpatialOSNestedComponent>>(entity))
                {
                    entityManager.AddComponentData(entity, new ComponentAdded<SpatialOSNestedComponent>());
                }
                else
                {
                    LogDispatcher.HandleLog(LogType.Error, new LogEvent(ReceivedDuplicateComponentAdded)
                        .WithField(LoggingUtils.LoggerName, LoggerName)
                        .WithField(LoggingUtils.EntityId, op.EntityId.Id)
                        .WithField("Component", "SpatialOSNestedComponent")
                    );
                }
            }

            public override void OnRemoveComponent(RemoveComponentOp op)
            {
                if (!IsValidEntityId(op.EntityId, "RemoveComponentOp", out var entity))
                {
                    return;
                }

                entityManager.RemoveComponent<SpatialOSNestedComponent>(entity);

                if (entityManager.HasComponent<ComponentAdded<SpatialOSNestedComponent>>(entity))
                {
                    entityManager.RemoveComponent<ComponentAdded<SpatialOSNestedComponent>>(entity);
                }
                else if (!entityManager.HasComponent<ComponentRemoved<SpatialOSNestedComponent>>(entity))
                {
                    entityManager.AddComponentData(entity, new ComponentRemoved<SpatialOSNestedComponent>());
                }
                else
                {
                    LogDispatcher.HandleLog(LogType.Error, new LogEvent(ReceivedDuplicateComponentRemoved)
                        .WithField(LoggingUtils.LoggerName, LoggerName)
                        .WithField(LoggingUtils.EntityId, op.EntityId.Id)
                        .WithField("Component", "SpatialOSNestedComponent")
                    );
                }
            }

            public override void OnComponentUpdate(ComponentUpdateOp op)
            {
                if (!IsValidEntityId(op.EntityId, "OnComponentUpdate", out var entity))
                {
                    return;
                }

                var data = entityManager.GetComponentData<SpatialOSNestedComponent>(entity);

                var update = global::Generated.Improbable.Gdk.Tests.SpatialOSNestedComponent.Serialization.GetAndApplyUpdate(op.Update.SchemaData.Value.GetFields(), ref data);

                List<SpatialOSNestedComponent.Update> updates;
                if (entityManager.HasComponent<SpatialOSNestedComponent.ReceivedUpdates>(entity))
                {
                    updates = entityManager.GetComponentData<SpatialOSNestedComponent.ReceivedUpdates>(entity).Updates;

                }
                else
                {
                    var updatesComponent = new SpatialOSNestedComponent.ReceivedUpdates
                    {
                        handle = ReferenceTypeProviders.UpdatesProvider.Allocate(World)
                    };
                    ReferenceTypeProviders.UpdatesProvider.Set(updatesComponent.handle, new List<SpatialOSNestedComponent.Update>());
                    updates = updatesComponent.Updates;
                    entityManager.AddComponentData(entity, updatesComponent);
                }

                updates.Add(update);

                data.DirtyBit = false;
                entityManager.SetComponentData(entity, data);
            }

            public override void OnAuthorityChange(AuthorityChangeOp op)
            {
                if (!IsValidEntityId(op.EntityId, "AuthorityChangeOp", out var entity))
                {
                    return;
                }

                ApplyAuthorityChange(entity, op.Authority, op.EntityId);
            }

            public override void OnCommandRequest(CommandRequestOp op)
            {
                if (!IsValidEntityId(op.EntityId, "CommandRequestOp", out var entity))
                {
                    return;
                }

                var commandIndex = op.Request.SchemaData.Value.GetCommandIndex();
                switch (commandIndex)
                {
                    default:
                        LogDispatcher.HandleLog(LogType.Error, new LogEvent(CommandIndexNotFound)
                            .WithField(LoggingUtils.LoggerName, LoggerName)
                            .WithField(LoggingUtils.EntityId, op.EntityId.Id)
                            .WithField("CommandIndex", commandIndex)
                            .WithField("Component", "SpatialOSNestedComponent")
                        );
                        break;
                }
            }

            public override void OnCommandResponse(CommandResponseOp op)
            {
                var commandIndex = op.Response.CommandIndex;
                switch (commandIndex)
                {
                    default:
                        LogDispatcher.HandleLog(LogType.Error, new LogEvent(CommandIndexNotFound)
                            .WithField(LoggingUtils.LoggerName, LoggerName)
                            .WithField(LoggingUtils.EntityId, op.EntityId.Id)
                            .WithField("CommandIndex", commandIndex)
                            .WithField("Component", "SpatialOSNestedComponent")
                        );
                        break;
                }
            }

            public override void AddCommandComponents(Unity.Entities.Entity entity)
            {
            }

            private void ApplyAuthorityChange(Unity.Entities.Entity entity, Authority authority, global::Improbable.Worker.EntityId entityId)
            {
                switch (authority)
                {
                    case Authority.Authoritative:
                        if (!entityManager.HasComponent<NotAuthoritative<SpatialOSNestedComponent>>(entity))
                        {
                            LogInvalidAuthorityTransition(Authority.Authoritative, Authority.NotAuthoritative, entityId);
                            return;
                        }

                        entityManager.RemoveComponent<NotAuthoritative<SpatialOSNestedComponent>>(entity);
                        entityManager.AddComponentData(entity, new Authoritative<SpatialOSNestedComponent>());

                        // Add event senders
                        break;
                    case Authority.AuthorityLossImminent:
                        if (!entityManager.HasComponent<Authoritative<SpatialOSNestedComponent>>(entity))
                        {
                            LogInvalidAuthorityTransition(Authority.AuthorityLossImminent, Authority.Authoritative, entityId);
                            return;
                        }

                        entityManager.AddComponentData(entity, new AuthorityLossImminent<SpatialOSNestedComponent>());
                        break;
                    case Authority.NotAuthoritative:
                        if (!entityManager.HasComponent<Authoritative<SpatialOSNestedComponent>>(entity))
                        {
                            LogInvalidAuthorityTransition(Authority.NotAuthoritative, Authority.Authoritative, entityId);
                            return;
                        }

                        if (entityManager.HasComponent<AuthorityLossImminent<SpatialOSNestedComponent>>(entity))
                        {
                            entityManager.RemoveComponent<AuthorityLossImminent<SpatialOSNestedComponent>>(entity);
                        }

                        entityManager.RemoveComponent<Authoritative<SpatialOSNestedComponent>>(entity);
                        entityManager.AddComponentData(entity, new NotAuthoritative<SpatialOSNestedComponent>());

                        // Remove event senders
                        break;
                }

                List<Authority> authorityChanges;
                if (entityManager.HasComponent<AuthorityChanges<SpatialOSNestedComponent>>(entity))
                {
                    authorityChanges = entityManager.GetComponentData<AuthorityChanges<SpatialOSNestedComponent>>(entity).Changes;

                }
                else
                {
                    var changes = new AuthorityChanges<SpatialOSNestedComponent>
                    {
                        Handle = AuthorityChangesProvider.Allocate(World)
                    };
                    AuthorityChangesProvider.Set(changes.Handle, new List<Authority>());
                    authorityChanges = changes.Changes;
                    entityManager.AddComponentData(entity, changes);
                }

                authorityChanges.Add(authority);
            }

            private bool IsValidEntityId(global::Improbable.Worker.EntityId entityId, string opType, out Unity.Entities.Entity entity)
            {
                if (!Worker.TryGetEntity(entityId, out entity))
                {
                    LogDispatcher.HandleLog(LogType.Error, new LogEvent(EntityNotFound)
                        .WithField(LoggingUtils.LoggerName, LoggerName)
                        .WithField(LoggingUtils.EntityId, entityId.Id)
                        .WithField("Op", opType)
                        .WithField("Component", "SpatialOSNestedComponent")
                    );
                    return false;
                }

                return true;
            }

            private void LogInvalidAuthorityTransition(Authority newAuthority, Authority expectedOldAuthority, global::Improbable.Worker.EntityId entityId)
            {
                LogDispatcher.HandleLog(LogType.Error, new LogEvent(InvalidAuthorityChange)
                    .WithField(LoggingUtils.LoggerName, LoggerName)
                    .WithField(LoggingUtils.EntityId, entityId.Id)
                    .WithField("New Authority", newAuthority)
                    .WithField("Expected Old Authority", expectedOldAuthority)
                    .WithField("Component", "SpatialOSNestedComponent")
                );
            }

        }

        public class ComponentReplicator : ComponentReplicationHandler
        {
            public override uint ComponentId => 20152;

            public override ComponentType[] ReplicationComponentTypes => new ComponentType[] {
                ComponentType.Create<SpatialOSNestedComponent>(),
                ComponentType.ReadOnly<Authoritative<SpatialOSNestedComponent>>(),
                ComponentType.ReadOnly<SpatialEntityId>()
            };

            public override ComponentType[] CommandTypes => new ComponentType[] {
            };


            public ComponentReplicator(EntityManager entityManager, Unity.Entities.World world) : base(entityManager)
            {
                var bookkeepingSystem = world.GetOrCreateManager<CommandRequestTrackerSystem>();
            }

            public override void ExecuteReplication(ComponentGroup replicationGroup, global::Improbable.Worker.Core.Connection connection)
            {
                var entityIdDataArray = replicationGroup.GetComponentDataArray<SpatialEntityId>();
                var componentDataArray = replicationGroup.GetComponentDataArray<SpatialOSNestedComponent>();

                for (var i = 0; i < componentDataArray.Length; i++)
                {
                    var data = componentDataArray[i];
                    var dirtyEvents = 0;

                    if (data.DirtyBit || dirtyEvents > 0)
                    {
                        var update = new global::Improbable.Worker.Core.SchemaComponentUpdate(20152);
                        SpatialOSNestedComponent.Serialization.Serialize(data, update.GetFields());

                        // Send serialized update over the wire
                        connection.SendComponentUpdate(entityIdDataArray[i].EntityId, new global::Improbable.Worker.Core.ComponentUpdate(update));

                        data.DirtyBit = false;
                        componentDataArray[i] = data;
                    }
                }
            }

            public override void SendCommands(List<ComponentGroup> commandComponentGroups, global::Improbable.Worker.Core.Connection connection)
            {
            }

        }

        public class ComponentCleanup : ComponentCleanupHandler
        {
            public override ComponentType[] CleanUpComponentTypes => new ComponentType[] {
                typeof(ComponentAdded<SpatialOSNestedComponent>),
                typeof(ComponentRemoved<SpatialOSNestedComponent>),
            };

            public override ComponentType[] EventComponentTypes => new ComponentType[] {
            };

            public override ComponentType ComponentUpdateType => ComponentType.ReadOnly<SpatialOSNestedComponent.ReceivedUpdates>();
            public override ComponentType AuthorityChangesType => ComponentType.ReadOnly<AuthorityChanges<SpatialOSNestedComponent>>();

            public override ComponentType[] CommandReactiveTypes => new ComponentType[] {
            };

            public override void CleanupUpdates(ComponentGroup updateGroup, ref EntityCommandBuffer buffer)
            {
                var entities = updateGroup.GetEntityArray();
                var data = updateGroup.GetComponentDataArray<SpatialOSNestedComponent.ReceivedUpdates>();
                for (var i = 0; i < entities.Length; i++)
                {
                    buffer.RemoveComponent<SpatialOSNestedComponent.ReceivedUpdates>(entities[i]);
                    ReferenceTypeProviders.UpdatesProvider.Free(data[i].handle);
                }
            }

            public override void CleanupAuthChanges(ComponentGroup authorityChangeGroup, ref EntityCommandBuffer buffer)
            {
                var entities = authorityChangeGroup.GetEntityArray();
                var data = authorityChangeGroup.GetComponentDataArray<AuthorityChanges<SpatialOSNestedComponent>>();
                for (var i = 0; i < entities.Length; i++)
                {
                    buffer.RemoveComponent<AuthorityChanges<SpatialOSNestedComponent>>(entities[i]);
                    AuthorityChangesProvider.Free(data[i].Handle);
                }
            }

            public override void CleanupEvents(ComponentGroup[] eventGroups, ref EntityCommandBuffer buffer)
            {
            }

            public override void CleanupCommands(ComponentGroup[] commandCleanupGroups, ref EntityCommandBuffer buffer)
            {
            }
        }
    }

}
