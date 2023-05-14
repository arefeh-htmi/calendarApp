import { storagePrefix } from '@/config'

export function getItemFromSessionStorage(key: string): unknown  {
  const item = window.sessionStorage.getItem(
    `${storagePrefix}_${key}`
  ) as string
  return item ? JSON.parse(item) : null
}

export function setItemInSessionStorage(name: string, data: unknown): void {
  const sessionName = `${storagePrefix}_${name}`
  window.sessionStorage.setItem(sessionName, JSON.stringify(data))
}

export function removeItemFromSessionStorage(name: string) {
  window.sessionStorage.removeItem(`${storagePrefix}_${name}`)
}
