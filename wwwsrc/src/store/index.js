import Vue from "vue";
import Vuex from "vuex";
import { api } from "../services/AxiosService.js";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    profile: {},
    blogs: [],
    profileBlogs: [],
    searchedProfile: {}
  },
  mutations: {
    setProfile(state, profile) {
      state.profile = profile;
    },
    setBlogs(state, blogs) {
      state.blogs = blogs
    },
    setSearchedProfile(state,profile){
      state.searchedProfile = profile
    },
    setProfileBlogs(state, blogs) {
      state.profileBlogs = blogs
    },

  },
  actions: {
    async getProfile({ commit }) {
      try {
        let res = await api.get("profiles");
        commit("setProfile", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getBlogs({ commit, dispatch }) {
      try {
        let res = await api.get("blogs")
        commit("setBlogs", res.data)
      } catch (error) {
        console.error(error)
      }
    },
    async createBlog({ commit, dispatch }, blogData) {
      try {
        let res = await api.post("blogs", blogData)
        dispatch("getBlogs")
      } catch (error) {
        console.error(error)
      }
    },
    async deleteBlog({ commit, dispatch }, blogId) {
      try {
        await api.delete("blogs/" + blogId)
        dispatch("getBlogs")
      } catch (error) {
        console.error(error)

      }
    },
    async getProfileBlogs({ commit, dispatch }, profileId) {
      try {
        let res = await api.get("profiles/" + profileId + "/blogs")
        commit("setProfileBlogs", res.data)
      } catch (error) {
        console.error(error)
      }
    },
    async getSearchedProfile({ commit, dispatch }, profileId) {
      try {
        let res = await api.get("profiles/" + profileId)
        commit("setSearchedProfile", res.data)
      } catch (error) {
        console.error(error)
      }
    }
  },
});
