import { App } from './App'
import { BrowserRouter } from 'react-router-dom'
import * as React from 'react'
import { createRoot } from 'react-dom/client'
const baseElement = document.getElementsByTagName('base')
const baseUrl = baseElement[0]?.getAttribute('href') ?? '/'
const rootElement = document.getElementById('root') as HTMLElement
const root = createRoot(rootElement)

root.render(
  <React.StrictMode>
    <BrowserRouter basename={baseUrl ?? ''}>
      <App />
    </BrowserRouter>
  </React.StrictMode>
)

