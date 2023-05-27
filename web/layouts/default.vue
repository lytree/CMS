<template>
  <div class="min-h-full flex h-screen font-roboto">
    <LayoutSidebar></LayoutSidebar>
    <div class="flex-1 flex flex-col overflow-hidden">
      <LayoutHeader></LayoutHeader>
      <main class="px-10px  text-center">
        <slot />
      </main>
      <LayoutFooter />
    </div>
  </div>
</template>
