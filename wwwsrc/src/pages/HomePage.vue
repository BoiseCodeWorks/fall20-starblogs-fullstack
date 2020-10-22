<template>
  <div class="home container-fluid">
    <h1>Welcome</h1>
    <div v-if="$auth.isAuthenticated">
      <form @submit.prevent="createBlog">
      <button type="submit">Create Blog</button>
      <input type="text" v-model="blogData.name">
      <input type="text" v-model="blogData.description">
      <input type="text" v-model="blogData.img">
      <input type="checkbox" v-model="blogData.published">
      </form>

    </div>
    <div class="row">
    <blog-component v-for="blog in blogs" :key="blog.id" :blogProp="blog"/>
    </div>
  </div>
</template>

<script>
import blogComponent from "../components/BlogComponent"
export default {
  name: "home",
  data() {
    return {
      blogData: {}
    }
  },
  computed: {
    blogs(){
      return this.$store.state.blogs
    }
  },
  methods: {
    createBlog(){
      this.$store.dispatch("createBlog", this.blogData);
    }
  },
  mounted(){
    this.$store.dispatch("getBlogs")
  },
  components:{blogComponent}
};
</script>
