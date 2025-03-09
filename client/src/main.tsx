import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import store from './state/redux/store';
import { Provider } from 'react-redux';
import AppRoutes from './routes';
import NotificationContainer from './containers/common/NotificationContainer';

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Provider store={store}>
      <NotificationContainer />
      <AppRoutes />
    </Provider>
  </StrictMode>,
);
