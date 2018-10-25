export const shuffle = (list)=> {
  for (let i = list.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [list[i], list[j]] = [list[j], list[i]];
  }
  return list;
}

const doubleDigitsToString = (num)=> num = num<10 ? `0${num}` : `${num}`

export const musicTimeFormat = (time)=> {
  const Timesec = Math.floor(time/1000)
  const min = doubleDigitsToString(Math.floor(Timesec/60))
  const sec = doubleDigitsToString(Timesec-min*60)
  return `${min}:${sec}`
}