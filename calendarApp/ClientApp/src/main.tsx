import { App } from './App'
import { BrowserRouter } from 'react-router-dom'
import * as serviceWorkerRegistration from './serviceWorkerRegistration'
import * as React from 'react'
import { createRoot } from 'react-dom/client'
import reportWebVitals from './reportWebVitals'

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

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://cra.link/PWA
serviceWorkerRegistration.unregister()

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals

reportWebVitals(() => {
  //just something 
})