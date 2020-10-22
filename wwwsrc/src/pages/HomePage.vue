<template>
  <div class="home">
    <h1>Welcome</h1>
    <div v-if="$auth.isAuthenticated">
      <button>Create Blog</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "home",
  computed: {},
  methods: {},
};
</script>
