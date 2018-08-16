
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
    public partial class ComponentWithNoFields
    {
        [ComponentId(1003)]
        internal class ReaderWriterCreator : IReaderWriterCreator
        {
            public IReaderWriterInternal CreateReaderWriter(Entity entity, EntityManager entityManager, ILogDispatcher logDispatcher)
            {
                return new ReaderWriterImpl(entity, entityManager, logDispatcher);
            }
        }

        [ReaderInterface]
        [ComponentId(1003)]
        public interface Reader : IReader<SpatialOSComponentWithNoFields, SpatialOSComponentWithNoFields.Update>
        {
        }

        [WriterInterface]
        [ComponentId(1003)]
        public interface Writer : IWriter<SpatialOSComponentWithNoFields, SpatialOSComponentWithNoFields.Update>
        {
        }

        internal class ReaderWriterImpl :
            BlittableReaderWriterBase<SpatialOSComponentWithNoFields, SpatialOSComponentWithNoFields.Update>, Reader, Writer
        {
            public ReaderWriterImpl(Entity entity,EntityManager entityManager,ILogDispatcher logDispatcher)
                : base(entity, entityManager, logDispatcher)
            {
            }

            protected override void TriggerFieldCallbacks(SpatialOSComponentWithNoFields.Update update)
            {
            }
            protected override void ApplyUpdate(SpatialOSComponentWithNoFields.Update update, ref SpatialOSComponentWithNoFields data)
            {
            }
        }
    }
}