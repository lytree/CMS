<template>
  <common-dark-mode-container class="absolute shadow-xl flex-center footer flex-shrink-0 lg:pl-64">
    <div text="xl gray4" m-5 flex="~ gap3" class="justify-center">
      <NuxtLink i-carbon-campsite to="/" />
      <a i-carbon-logo-github href="https://github.com/antfu/vitesse-nuxt3" target="_blank" />
      <common-dark-toggle />
    </div>
  </common-dark-mode-container>
</template>
<style scoped>
.footer {
  height: 56px;
  z-index: 20;
  left: 0;
  bottom: 0;
  box-sizing: 'border-box';
  width: '100%';
}
</style>