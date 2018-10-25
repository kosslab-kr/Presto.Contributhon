import Vue from "vue";
import VueRouter from "vue-router";
import HomePage from "./components/HomePage";
import AlbumPage from "./components/AlbumPage";
import ArtistsPage from "./components/ArtistsPage";
import GenrePage from "./components/GenrePage";
import NotFound from "./components/NotFound";

Vue.use(VueRouter);

const routes = [
  { path: "/", component: HomePage },
  { path: "/album", component: AlbumPage },
  { path: "/artists", component: ArtistsPage },
  { path: "/genres", component: GenrePage },
  { path: "*", component: NotFound }
];

const router = new VueRouter({
  scrollBehavior: () => ({ y: 0 }),
  routes
});

export default router;
