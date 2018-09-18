import { $on } from "./helper/domHelper.js";
import FormView from "./View/FormView.js";
import MusicPlayer from './MusicPlayer/MusicPlayer.js';

$on(document, "DOMContentLoaded", () => {
  const form = new FormView({
    searchFormSelector: ".search__form",
    searchInputSelector: ".search__input",
  });

  const drawer__wrapper = document.querySelector(".drawer__wrapper");
  const hamburgerBtn = document.querySelector(".hamburger");
  hamburgerBtn.addEventListener("click", e => {
    hamburgerBtn.classList.toggle("active");
    drawer__wrapper.classList.toggle("active");
  });
  const musicPlayer  = new MusicPlayer({
    audio: '#fotter-player',
    playBtn: '#footer-playBtn',
    pauseBtn: '#footer-pauseBtn',
    timer: '.start-timer',
    progressBar: '.progressbar',
    endTime: '.finish-time',
    rangeBar: '.range-bar',
  })
  
});
