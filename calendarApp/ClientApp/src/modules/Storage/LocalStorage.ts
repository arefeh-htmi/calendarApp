import { storagePrefix } from '@/config'

export function getItemFromLocalStorage(key: string) {
  const item = window.localStorage.getItem(`${storagePrefix}_${key}`) as string
  return item ? JSON.parse(item) : null
}

export function setItemInLocalStorage(name: string, data: any) {
  window.localStorage.setItem(
    `${storagePrefix}_${name}` as string,
    JSON.stringify(data)
  )
}

export function removeItemFromLocalStorage(name: string) {
  window.localStorage.removeItem(`${storagePrefix}_${name}`)
}
