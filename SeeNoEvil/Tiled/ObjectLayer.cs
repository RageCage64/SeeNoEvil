using System;
using System.Collections.Generic;
using System.Linq;

//TODO Do all these need to be public?
namespace SeeNoEvil.Tiled {
    //TODO Can probably be a struct?
    public class ObjectLayer {
        public IEnumerable<ObjectCoordinate> Objects {get; private set;}
        public string Name {get; private set;}
        public ObjectLayer(MapLayerModel model) {
            Name = model.Name;
            Objects = model.Objects.Select(o => new ObjectCoordinate(o.Id,
                                                                     o.X,
                                                                     o.Y));
        }
    }

    public struct ObjectCoordinate {
        public ObjectCoordinate(int id, float x, float y) {
            X = (int)Math.Round(x / 32)*32;
            Y = (int)Math.Round(y / 32)*32;
            Id = id;
        }

        public int X {get; set;}
        public int Y {get; set;}
        public int Id {get; set;}
    }
}