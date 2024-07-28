import { PropsWithChildren } from 'react';
import { RequiredAuthLayout } from '@socnet/widgets';

const Layout = ({ children }: PropsWithChildren) => {
  return (
    <RequiredAuthLayout>
      <header>HEADER</header>
      <nav>NAVBAR</nav>
      <main>{children}</main>
    </RequiredAuthLayout>
  );
};

export default Layout;
