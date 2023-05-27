<script setup lang="ts">
import { appName } from '~/constants'

useHead({
  title: appName,
})
</script>

<template>
  <NuxtLayout >
    <NuxtLoadingIndicator :height="5" :duration="3000" :throttle="400" /> <!-- I'm here -->
    <NuxtPage />
  </NuxtLayout>
</template>

<style>
html,
body,
#__nuxt {
  height: 100%;
  margin: 0;
  padding: 0;
}

html.dark {
  background: #101014;
  color: white;
}

html.light {
  background: #f6f9f8;
  color: black;
}
</style>
