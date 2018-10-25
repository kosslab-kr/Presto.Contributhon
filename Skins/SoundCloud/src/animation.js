const animations = {
  fadeOut(el, change = 0.05) {
    let opacity = 1;
    (function decreaseOpacity() {
      opacity -= change;
      el.style.opacity = opacity;
      if (opacity <= 0) {
        el.style.opacity = 0;
        return true;
      }
      requestAnimationFrame(decreaseOpacity);
    }());
  },
  fadeIn(el, change = 0.05) {
    let opacity = 0;
    (function IncreaseOpacity() {
      opacity += change;
      el.style.opacity = opacity;
      if (opacity > 1) {
        el.style.opacity = 1;
        return true;
      }
      requestAnimationFrame(IncreaseOpacity);
    }());
  },
  progressBar(music, changeHandler){
    (function updateProgressBar() {
      if (music.currentTime === music.runningTime) { 
        return true;
      }
      changeHandler()
      requestAnimationFrame(updateProgressBar);
    }());
  }
};

export default animations;