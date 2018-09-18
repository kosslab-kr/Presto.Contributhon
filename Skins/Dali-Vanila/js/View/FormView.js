import { qs, $on } from "../helper/domHelper.js";

class FormView {
  constructor({ searchFormSelector, searchInputSelector }) {
    this.searchFormEl = qs(searchFormSelector);
    this.searchInputEl = qs(searchInputSelector);
    this.bindEvents();
    this.notifyKeyWordSubmit = null;
  }
  bindEvents() {
    $on(this.searchFormEl, "submit", e => this.hanldeSubmit(e));
    $on(this.searchFormEl, "blur", () => this.clearInput());
  }
  hanldeSubmit(e) {
    e.preventDefault();
    const keyword = this.searchInputEl.value.trim();
    keyword && this.handlePost(keyword);
  }
  handlePost(keyword) {
    console.log(`submit this ${keyword}`);
    this.clearInput();
  }
  clearInput() {
    this.searchInputEl.value = "";
  }
}

export default FormView;
