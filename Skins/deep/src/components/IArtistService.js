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

const Homesick = {
  title: 'Homesick',
  artist: {name: 'Kings of Convenience'},
  album: {
    name: 'Riot On An Empty Street',
    picture: require('../assets/album_picture/RiotOnAnEmptyStreet.jpg')
  },
  genre: {name: 'POP'},
  length: 193000,
  previous: null,
  next: null,
};

Misread.next = Homesick;
Homesick.next = Misread;

Misread.previous = Homesick;
Homesick.previous = Misread;

const KingsOfConvenience = {
  name: 'Kings of Convenience',
  musics: [Misread, Homesick],
}

export default {
  artists: [KingsOfConvenience],
}