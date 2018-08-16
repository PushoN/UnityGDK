
// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System;
using System.Collections.Generic;
using Unity.Entities;
using Improbable.Gdk.Core;
using Improbable.Gdk.Core.GameObjectRepresentation;
using Entity = Unity.Entities.Entity;

namespace Generated.Improbable.Gdk.Tests.ComponentsWithNoFields
{
    public partial class ComponentWithNoFieldsWithCommands
    {
        [ComponentId(1005)]
        internal class ReaderWriterCreator : IReaderWriterCreator
        {
            public IReaderWriterInternal CreateReaderWriter(Entity entity, EntityManager entityManager, ILogDispatcher logDispatcher)
            {
                return new ReaderWriterImpl(entity, entityManager, logDispatcher);
            }
        }

        [ReaderInterface]
        [ComponentId(1005)]
        public interface Reader : IReader<SpatialOSComponentWithNoFieldsWithCommands, SpatialOSComponentWithNoFieldsWithCommands.Update>
        {
        }

        [WriterInterface]
        [ComponentId(1005)]
        public interface Writer : IWriter<SpatialOSComponentWithNoFieldsWithCommands, SpatialOSComponentWithNoFieldsWithCommands.Update>
        {
        }

        internal class ReaderWriterImpl :
            BlittableReaderWriterBase<SpatialOSComponentWithNoFieldsWithCommands, SpatialOSComponentWithNoFieldsWithCommands.Update>, Reader, Writer
        {
            public ReaderWriterImpl(Entity entity,EntityManager entityManager,ILogDispatcher logDispatcher)
                : base(entity, entityManager, logDispatcher)
            {
            }

            protected override void TriggerFieldCallbacks(SpatialOSComponentWithNoFieldsWithCommands.Update update)
            {
            }
            protected override void ApplyUpdate(SpatialOSComponentWithNoFieldsWithCommands.Update update, ref SpatialOSComponentWithNoFieldsWithCommands data)
            {
            }

            public void OnCmdCommandRequest(Cmd.Request request)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}