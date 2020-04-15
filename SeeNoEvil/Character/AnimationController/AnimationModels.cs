using System.Collections.Generic;

namespace SeeNoEvil.Character {
    public struct AnimationSetModel {
        public string Name {get; set;}
        public string Image {get; set;}
        public int Width {get; set;}
        public int Height {get; set;}
        public IEnumerable<AnimationModel> Animations {get; set;}
    }

    public struct AnimationModel {
        public int Id {get; set;}
        public string Name {get; set;}
        public IEnumerable<Frame> Frames{get; set;}
    }

    public struct Frame {
        public int Id {get; set;}
        public int X {get; set;}
        public int Y {get; set;}
        public int Timer {get; set;}
    }
}