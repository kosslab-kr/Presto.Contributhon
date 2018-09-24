const Nevermind_Nirvana = {
  title: 'Nevermind',
  artist: 'Nirvana',
  picture: require('./assets/album_picture/Nevermind.jpg'),
  musics: null
}

Nevermind_Nirvana.musics = [
  {
    title: 'Smells Like Teen Spirit',
    artist: 'Nirvana',
    runningTime: 302000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  },
  {
    title: 'In Bloom - Nevermind Vesion',
    artist: 'Nirvana',
    runningTime: 255000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  },
  {
    title: 'Come As You Are',
    artist: 'Nirvana',
    runningTime: 219000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  },
  {
    title: 'Breed',
    artist: 'Nirvana',
    runningTime: 184000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  },
  {
    title: 'Lithium',
    artist: 'Nirvana',
    runningTime: 257000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  },
  {
    title: 'Polly',
    artist: 'Nirvana',
    runningTime: 174000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  },
  {
    title: 'Territorial Pissings',
    artist: 'Nirvana',
    runningTime: 143000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  },
  {
    title: 'Drain You',
    artist: 'Nirvana',
    runningTime: 224000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  },
  {
    title: 'Lounge Act',
    artist: 'Nirvana',
    runningTime: 156000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  },
  {
    title: 'Stay Away',
    artist: 'Nirvana',
    runningTime: 211000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  },
  {
    title: 'On A Plain',
    artist: 'Nirvana',
    runningTime: 194000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  },
  {
    title: 'Somethind In The Way',
    artist: 'Nirvana',
    runningTime: 232000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  },
  {
    title: 'Endless, Nameless',
    artist: 'Nirvana',
    runningTime: 403000,
    album: Nevermind_Nirvana,
    genre: 'ROCK'
  }
]

const ThePastThePresentThePossible_Tahiti80 = {
  title: "The Past, The Present & The Possible",
  artist: "Tahiti 80",
  picture: require('./assets/album_picture/ThePastThePresentThePossible.jpg'),
  musics: null
}

ThePastThePresentThePossible_Tahiti80.musics = [
  {
    title: 'Denfender',
    artist: 'Tahiti 80',
    runningTime: 248000,
    album: ThePastThePresentThePossible_Tahiti80,
    genre: 'POP'
  },
  {
    title: "Darlin' (Adam & Eve Song)",
    artist: 'Tahiti 80',
    runningTime: 199000,
    album: ThePastThePresentThePossible_Tahiti80,
    genre: 'POP'
  },
  {
    title: 'Gate 33',
    artist: 'Tahiti 80',
    runningTime: 232000,
    album: ThePastThePresentThePossible_Tahiti80,
    genre: 'POP'
  },
  {
    title: 'Solitary Bizness',
    artist: 'Tahiti 80',
    runningTime: 249000,
    album: ThePastThePresentThePossible_Tahiti80,
    genre: 'POP'
  },
  {
    title: 'Want Some?',
    artist: 'Tahiti 80',
    runningTime: 214000,
    album: ThePastThePresentThePossible_Tahiti80,
    genre: 'POP'
  },
  {
    title: 'Easy',
    artist: 'Tahiti 80',
    runningTime: 192000,
    album: ThePastThePresentThePossible_Tahiti80,
    genre: 'POP'
  },
  {
    title: 'The Past, The Present & The Possible',
    artist: 'Tahiti 80',
    runningTime: 325000,
    album: ThePastThePresentThePossible_Tahiti80,
    genre: 'POP'
  },
  {
    title: 'Nightmares',
    artist: 'Tahiti 80',
    runningTime: 397000,
    album: ThePastThePresentThePossible_Tahiti80,
    genre: 'POP'
  },
  {
    title: 'Rain, Stream & Speed',
    artist: 'Tahiti 80',
    runningTime: 174000,
    album: ThePastThePresentThePossible_Tahiti80,
    genre: 'POP'
  },
  {
    title: 'Crack Up(Extended Mix)',
    artist: 'Tahiti 80',
    runningTime: 483000,
    album: ThePastThePresentThePossible_Tahiti80,
    genre: 'POP'
  },
  {
    title: '4:00 AM',
    artist: 'Tahiti 80',
    runningTime: 220000,
    album: ThePastThePresentThePossible_Tahiti80,
    genre: 'POP'
  },
]

const library = {
  albums: [Nevermind_Nirvana, ThePastThePresentThePossible_Tahiti80]
}

const musics = library.albums.reduce((musics, album) => musics.concat(album.musics), []);

const genres = musics.reduce((genres, music) => {
  for(let genre of genres) {
    if(genre.type !== music.genre) continue;

    genre.musics = genre.musics || [];
    genre.musics.push(music);

    return genres;
  }

  // if genre is not exist
  return genres.concat({ type: music.genre, musics: [music]});
}, []);

library.musics = musics;
library.genres = genres;

export default library;