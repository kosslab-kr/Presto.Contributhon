export const qs = (selector, scope = document) => scope.querySelector(selector);

export const qsAll = (selector, scope = document) => scope.querySelectorAll(selector);

export const $on = (target, type, callback, capture = false) => target.addEventListener(type, callback, !!capture);
