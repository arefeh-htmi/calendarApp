import { Layout } from './components/Layout/Layout'
import { MainRoutes } from '@routes/Main'
export function App(): JSX.Element {
  return (
    <Layout>
      <MainRoutes />
    </Layout>
  )
}
