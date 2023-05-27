<script setup lang="ts">
const route = useRoute()
const user = useUserStore()
// const name = route.params.id

watchEffect(() => {
    user.setNewName(route.params.id as string)
})

definePageMeta({
    layout: 'default',
})
</script>

<template>
    <div>
        <div class="i-twemoji:waving-hand inline-block animate-shake-x animate-duration-5000 text-4xl" />
        <h3 class="text-2xl font-500">
            Hi,
        </h3>
    </div>
</template>
