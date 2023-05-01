import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './routes/AppRoutes';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import { Layout } from './components/Layout';

export function App() {

  return (
    <Layout>
    <Routes>
      {AppRoutes.map((route, index) => {
        const { element, requireAuth, ...rest } = route;
        return <Route key={index} {...rest} element={requireAuth ? <AuthorizeRoute {...rest} element={element} /> : element} />;
      })}
    </Routes>
  </Layout>
  )
}

