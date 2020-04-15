using System.Collections.Generic;
using System.Linq;

namespace SeeNoEvil.Character {
    public class AnimationController {
        public string Name {get; private set;}
        public string Image {get; private set;}
        public int Width {get; private set;}
        public int Height {get; private set;}
        public Dictionary<int, Animation> Animations {get; private set;}
        private Animation CurrentAnimation;
        public Frame CurrentFrame => 
            CurrentAnimation.GetFrame(); 

        public AnimationController(AnimationSetModel model) {
            Name = model.Name;
            Image = model.Image;
            Width = model.Width;
            Height = model.Height;             
            Animations = model.Animations.Aggregate(new Dictionary<int, Animation>(),
                (animations, animation) => {
                    animations.Add(animation.Id, new Animation(animation));
                    return animations;
                });
            ChangeAnimation(1);
        }

        public void ChangeAnimation(int animationId) {
            if(Animations.TryGetValue(animationId, out CurrentAnimation)) 
                CurrentAnimation.Reset();
        } 
    }

    public class Animation {
        public int Id {get; set;}
        public string Name {get; set;}
        public FrameCollection Frames {get; set;}
        private int TotalFrames;
        private Frame CurrentFrame;
        private int CurrentFrameId;

        public Animation(AnimationModel model) {
            Id = model.Id;
            Name = model.Name;
            Frames = new FrameCollection(model.Frames);
            TotalFrames = model.Frames.Count();
        }

        public void Reset() {
            CurrentFrameId = 1;
            CurrentFrame = Frames[CurrentFrameId];
        }

        // TODO Is this super fuckin ugly? Seems like it
        public Frame GetFrame() {
            Frame result = CurrentFrame;
            if(CurrentFrame.Timer == 1) {
                CurrentFrameId = CurrentFrameId == TotalFrames ? 1 : CurrentFrameId + 1;
                CurrentFrame = Frames[CurrentFrameId];
            } else if(CurrentFrame.Timer > 1) CurrentFrame.Timer--;
            return result;
        }
    }

    public class FrameCollection {
        private IEnumerable<Frame> Frames;

        public Frame this[int i] {
            get {
                return Frames.Where(item => item.Id == i).First();
            }
        }

        public FrameCollection(IEnumerable<Frame> frames) {
            Frames = frames;
        }
    }
}