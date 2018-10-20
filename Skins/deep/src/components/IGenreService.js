const Gate33 = {
  title: 'Gate 33',
  artist: {name: 'Tahiti 80'},
  album: {
    name: 'The Past, The Present & The Possible',
    picture: require('../assets/album_picture/ThePastThePresentThePossible.jpg')
  },
  genre: {name: 'POP'},
  length: 232000,
  previous: null,
  next: null,
};

const Misread = {
  title: 'Misread',
  artist: {name: 'Kings of Convenience'},
  album: {
    name: 'Riot On An Empty Street',
    picture: require('../assets/album_picture/RiotOnAnEmptyStreet.jpg')
  },
  genre: {name: 'POP'},
  length: 188000,
  previous: null,
  next: null,
};

Gate33.next = Misread;
Misread.next = Gate33;

Gate33.previous = Misread;
Misread.previous = Gate33;

const POP = {
  name: 'POP',
  musics: [Gate33, Misread],
};

const SmellsLikeTeenSpirit = {
  title: 'Smells Like Teen Spirit',
  artist: {name: 'Nirvana'},
  album: {
    name: 'Nevermind', 
    picture: require('../assets/album_picture/Nevermind.jpg'),
  },
  genre: {name: 'ROCK'},
  length: 302000,
  previous: null,
  next: null,
};

const InBloomNevermindVersion = {
  title: 'In Bloom - Nevermind Version',
  artist: {name: 'Nirvana'},
  album: {
    name: 'Nevermind',
    picture: require('../assets/album_picture/Nevermind.jpg'),
  },
  genre: {name: 'ROCK'},
  length: 255000,
  previous: null,
  next: null,
};

SmellsLikeTeenSpirit.next = InBloomNevermindVersion;
InBloomNevermindVersion.next = SmellsLikeTeenSpirit;

SmellsLikeTeenSpirit.previous = InBloomNevermindVersion;
InBloomNevermindVersion.previous = SmellsLikeTeenSpirit;

const HIPHOP = {
  name: 'ROCK',
  musics: [SmellsLikeTeenSpirit, InBloomNevermindVersion],
};

export default {
  genres: [POP, HIPHOP],
}