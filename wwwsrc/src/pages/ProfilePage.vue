<template>
  <div class="profile container-fluid">
    <h1>Welcome to {{profile.name}}'s Page</h1>
    <div class="row">
    <blog-component v-for="blog in blogs" :key="blog.id" :blogProp="blog" />

    </div>
  </div>
</template>

<script>
import blogComponent from "../components/BlogComponent";
export default {
  name: "profile",
  computed: {
    blogs() {
      return this.$store.state.profileBlogs;
    },
    profile(){
      return this.$store.state.searchedProfile
    }
  },
  mounted() {
      this.$store.dispatch("getProfileBlogs", this.$route.params.profileId);
      this.$store.dispatch("getSearchedProfile", this.$route.params.profileId);
  },
  methods: {},
  components: { blogComponent },
};
</script>
