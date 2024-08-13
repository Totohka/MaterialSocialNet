import type { Meta, StoryObj } from '@storybook/react';

const Example = () => {
  return (
    <div>
      <h1>Example</h1>
    </div>
  );
};

const meta: Meta<typeof Example> = {
  component: Example,
};

export default meta;
type Story = StoryObj<typeof Example>;
export const Primary: Story = {
  render: () => <Example />,
};
