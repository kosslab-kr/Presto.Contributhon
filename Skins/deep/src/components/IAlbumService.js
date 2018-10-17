const SmellsLikeTeenSpirit = {
  title: 'Smells Like Teen Spirit',
  artist: {name: 'Nirvana'},
  album: {picture: require('../assets/album_picture/Nevermind.jpg'),},
  genre: {name: 'ROCK'},
  length: 302000,
  previous: null,
  next: null,
};

const InBloomNevermindVersion = {
  title: 'In Bloom - Nevermind Version',
  artist: {name: 'Nirvana'},
  album: {picture: require('../assets/album_picture/Nevermind.jpg'),},
  genre: {name: 'ROCK'},
  length: 255000,
  previous: null,
  next: null,
};

const ComeAsYouAre = {
  title: 'Come As You Are',
  artist: {name: 'Nirvana'},
  album: {picture: require('../assets/album_picture/Nevermind.jpg'),},
  genre: {name: 'ROCK'},
  length: 219000,
  previous: null,
  next: null,
};

SmellsLikeTeenSpirit.next = InBloomNevermindVersion;
InBloomNevermindVersion.next = ComeAsYouAre;
ComeAsYouAre.next = SmellsLikeTeenSpirit;

SmellsLikeTeenSpirit.previous = ComeAsYouAre;
ComeAsYouAre.previous = InBloomNevermindVersion;
InBloomNevermindVersion.previous = SmellsLikeTeenSpirit

const Nevermind_Nirvana = {
  name: 'Nevermind',
  artist: {name: 'Nirvana'},
  picture: require('../assets/album_picture/Nevermind.jpg'),
  musics: [SmellsLikeTeenSpirit, InBloomNevermindVersion, ComeAsYouAre],
};

export default {
  albums: [Nevermind_Nirvana],
}