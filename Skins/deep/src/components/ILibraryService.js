const SmellsLikeTeenSpirit = {
  title: 'Smells Like Teen Spirit',
  artist: {name: 'Nirvana'},
  album: {picture: require('../assets/album_picture/Nevermind.jpg'),},
  genre: {name: 'ROCK'},
  length: 302000,
  previous: null,
  next: null,
};

const Gate33 = {
  title: 'Gate 33',
  artist: {name: 'Tahiti 80'},
  album: {picture: require('../assets/album_picture/ThePastThePresentThePossible.jpg')},
  genre: {name: 'POP'},
  length: 232000,
  previous: null,
  next: null,
}

const Misread = {
  title: 'Misread',
  artist: {name: 'Kings of Convenience'},
  album: {picture: require('../assets/album_picture/RiotOnAnEmptyStreet.jpg')},
  genre: {name: 'POP'},
  length: 188000,
  previous: null,
  next: null,
}

SmellsLikeTeenSpirit.next = Gate33;
Gate33.next = Misread;
Misread.next = SmellsLikeTeenSpirit;

SmellsLikeTeenSpirit.previous = Misread;
Misread.previous = Gate33;
Gate33.previous = SmellsLikeTeenSpirit;

export default {
  musics: [SmellsLikeTeenSpirit, Misread, Gate33],
}