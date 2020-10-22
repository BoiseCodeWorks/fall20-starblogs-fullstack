<template>
  <div class="blog-component card col-3">
    <i class="fa fa-times text-danger" v-if="profile.email == blogProp.creatorEmail" @click="deleteBlog" aria-hidden="true"></i>
    <img :src="blogProp.img " alt="">
    <h2>{{blogProp.name}}</h2>
    <p>{{blogProp.description}}</p>
    <p>{{blogProp.creatorEmail}}</p>
    <img :src="blogProp.creator.picture" @click="viewProfile" alt="">
  </div>
</template>


<script>
export default {
  name: 'blog-component',
  data(){
    return {}
  },
  computed:{
    profile(){
      return this.$store.state.profile
    }
  },
  methods:{
    deleteBlog(){
      this.$store.dispatch("deleteBlog",this.blogProp.id)
    },
    viewProfile(){
      this.$router.push({name: "Profile", params: { profileId:this.blogProp.creator.id}})
    },
  },
  props:["blogProp"],
  components:{}
}
</script>


<style scoped>

</style>