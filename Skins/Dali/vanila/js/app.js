import { $on } from "./helper/domHelper.js";
import FormView from "./View/FormView.js";

$on(document, "DOMContentLoaded", () => {
  const form = new FormView({
    searchFormSelector: ".search__form",
    searchInputSelector: ".search-input",
  });
});
