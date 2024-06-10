import './App.css';
import { Box, Container } from '@mui/material';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import { Home } from './pages/home';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
  },
]);

function App() {
  return (
    <Container maxWidth="sm">
      <Box sx={{ my: 4 }}>
        <RouterProvider router={router} />
      </Box>
    </Container>
  );
}

export default App;
